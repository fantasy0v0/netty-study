package com.github.fantasy0v0.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.ByteToMessageCodec;

import java.util.List;

public class LongToStringDecoder extends ByteToMessageCodec {
  @Override
  protected void encode(ChannelHandlerContext ctx, Object msg, ByteBuf out) throws Exception {

  }

  @Override
  protected void decode(ChannelHandlerContext ctx, ByteBuf in, List out) throws Exception {

  }
}
